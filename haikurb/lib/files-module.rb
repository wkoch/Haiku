# Module containing all kind of File manipulation functions
# Version 0.1
# 07/12/2013

require 'fileutils'

module Files
  def new_file(filename,extension=false,folder=false)
    unless folder
      unless extension
        if replace_file?(filename)
          file = File.new(filename)
      else
        file = File.new ("#{filename}.#{extension}")
      end
    else
      folder = folder + "/" unless folder.end_with?("/")
      file = File.new("#{folder}")
    end
  end

  def open_file
  end

  def delete_file
  end

  def replace_file?(filename)
    if File.exists?(filename)
      ask_user
  end
end











==========================
#!/usr/bin/env ruby -wKU
# haiku v0.3.1 11/2013 ~ William Koch

require 'kramdown'

class Haiku
  private
  def help
    puts "",
         "       Haiku v0.3.1 - 11/2013",
         "",
         "      help: Shows this Help text.",
         "        new: Creates a new Draft.",
         "       build: Builds this blog",
         "    publish: Publish you latest Draft.",
         "publish all: Publishes all Drafts.",""
  end

  def ask_user(message,type="text") # Ask a questions, returns the answer.
    if type.downcase == "y/n"
      print message + " (y/N) "
      gets().chomp.downcase == ("y" || "yes") ? new_draft : build
    elsif type.downcase == "text"
      print message + " "
      gets.chomp
    end
  end

  def date # Uses my prefered date format, YYYY-MM-DD.
    today = Time.new
    year, month, day = today.year, today.month, today.day
    "#{year}-#{fix_date(month)}-#{fix_date(day)}"
  end

  def fix_date(date) # Return all values as a two digits string.
    date.to_s.size > 1 ? date : "0#{date}"
  end

  def linkify(post_address) # Removes bad characters from the address string.
    bad_chars = {a: ["á", "à", "â", "ã", "ä"],
                 e: ["é", "è", "ê", "ẽ", "ë"],
                 i: ["í", "ì", "î", "ĩ", "ï"],
                 o: ["ó", "ò", "ô", "õ", "ö"],
                 u: ["ú", "ù", "û", "ũ", "ü"],
                 c: ["ç"]}
    post_address.gsub!(/\s/, "_")
    while post_address.match(/\W+/)
      bad_chars.each do |key, values|
        values.each do |char|
          post_address.gsub!(char, key.to_s)
        end
      end
    end
    return post_address.gsub!(/_/, "-")
  end

  public

  def start # Everything starts here.
    argument_1 = ARGV[0]
    argument_2 = ARGV[1]||0
    ARGV.clear

    case argument_1
      when "new"     then new_draft
      when "build"   then build
      when "publish" then publish(argument_2)
      when "help"    then help
      else build
    end
  end
end

class Blog < Haiku
  attr_accessor :title, :logo, :posts

  def initialize
    @posts = []
  end

  private

  def new_draft # Makes a Draft file for a new post, with date, address and title.
    new_draft = Post.new
    new_draft.address = ask_user("What should be this post's address?").downcase
    new_draft.title = ask_user("What should be the title?")
    new_file = "drafts/#{date}-#{linkify(new_draft.address)}.md"
    if File.exists?(new_file)
      raise("Address already in use!!")
    else
      puts "", "Creating new draft..."
      draft = File.new(new_file, "w")
      draft.puts "#{new_draft.title}", "", ""
      draft.close
      puts "Draft successfuly created.", ""
    end
  end

  def publish(draft_id) # Publishes the latest Draft, or all of them.
    drafts = list("drafts")
    if draft_id == "all"
      puts "Publishing all drafts.","..."
      drafts.each do |file|
        FileUtils.mv("drafts/#{file}", "posts/#{file}")
      end
      puts "done."
    else
      puts "Publishing #{drafts.first}.","..."
      FileUtils.mv("drafts/#{drafts.first}", "posts/#{drafts.first}")
      puts "Done."
    end
  end

  def build # Builds everything, clean all older files, list published posts, load layouts, makes index and posts.
    puts "", "Building...", "", "Wow, it looks awesome!!"
    clean
    list("posts")
    layout
    create_index
    create_posts
  end

  def clean # Removes all html files from the blog folder.
    FileUtils.rm Dir.glob('../*.html')
  end

  def list(folder_name) # Lists all .md files in specified folder.
    listing_post_files = Dir.glob("#{folder_name}/*.md")
    listing_post_files.each do |file|
      file.sub!("#{folder_name}/", '')
    end
    listing_post_files.sort!{|x,y| y <=> x }.each do |a_post| # Commom Blog order, newest to oldest.
      if folder_name == "posts"
        post = Post.new
        @posts << post
        post.populate(a_post)
      else
        return listing_post_files
      end
    end
  end

  def layout # Loads and prepares all layout files
    @layout_page = File.new("layouts/layout.html").read.sub("{ blog_title }", @title).sub("{ blog_logo }", @logo)
    @index_page = File.new("layouts/index.html").read
    @index_posts = File.new("layouts/index_posts.html").read
    @post_page = File.new("layouts/post.html").read
  end

  def create_index # Creates index.html with a list of posts.
    index = File.new("../index.html", "w")
    list_of_posts ||= ""
    @posts.each do |post|
      list_of_posts += @index_posts.gsub("{ date }", post.date).gsub("{ address }", post.address).gsub("{ title }", post.title)
    end
    index.puts @layout_page.sub("{ yield }", @index_page.sub("{ yield }", list_of_posts))
    index.close
  end

  def create_posts # Creats html pages for each post.
    @posts.each do |post|
      post_page = File.new("../#{post.address}", "w")
      post_page.puts @layout_page.sub("{ yield }", @post_page.gsub("{ title }", post.title).gsub("{ date }", post.date).gsub("{ text }", post.text))
      post_page.close
    end
  end
end

class Post < Blog
  attr_accessor :address, :title, :date, :text

  def populate(a_post) # Loads the data from a post *.md file.
    post_contents = File.new("posts/#{a_post}", "r").read
    post_array = []
    post_contents.each_line do |line|
      post_array << line
    end
    @address = a_post.to_s.sub("md", "html")
    @title = post_array[0].chomp
    @date = a_post.match(/[0-9]{4}-[0-9]{2}-[0-9]{2}/).to_s.chomp
    @text = Kramdown::Document.new(post_contents.sub!("#{title}","").chomp).to_html.chomp
  end
end

blog = Blog.new
blog.title, blog.logo = "William Koch / Blog", "~/Blog"
blog.start