#!/usr/bin/env ruby -wKU

# haiku v0.2

# Author: William Koch
# Date: 08/2013

require 'kramdown'
require 'fileutils'

#### Functions
def fix_date(date)
	date.to_s.size > 1 ? date : "0#{date}"
end

def ask_user(message,type="text")
  if type.downcase == "y/n"
    print message + " (y/N) "
    if gets().chomp.downcase == ("y" || "yes")
      new_post
    else
      build_blog
    end
  elsif type.downcase == "text"
    print message + " "
    gets.chomp
  end
end

def new_post
  post_address = ask_user("What should be this post's address?")
  post_title = ask_user("What is the title of your post?")
  new_file = "drafts/#{@date}-#{post_address}.md"
  if File.exists?(new_file)
    raise("File already exists!!")
  else
    puts "", "Creating new post..."
    new_post = File.new(new_file, "w")
    new_post.puts "#{post_title}", "", ""
    new_post.close
    puts "Post successfuly created.", ""
  end
end

def clean_older_files
  # Remove every html files from the blog folder.
  FileUtils.rm Dir.glob('../*.html')
end

def list_posts
  list_of_posts = [] & @posts = []
  Dir.foreach("posts") {|post| list_of_posts.push(post) }
  list_of_posts.delete_if {|y| y == "." or y == ".." }
  list_of_posts.sort{|x,y| y <=> x }.each do |post|
    file = File.new("posts/#{post}", "r")
    post_contents = file.read
    post_array = []
    post_contents.each_line do |line|
      post_array << line
    end
    title = post_array[0]
    date = post.match(/[0-9]{4}-[0-9]{2}-[0-9]{2}/).to_s.chomp
    post_contents.sub!("#{title}","")
    @posts << post = {:address => post.to_s.sub("md", "html"),:title => title.chomp, :date => date, :text => Kramdown::Document.new(post_contents.chomp).to_html.chomp}
  end
end

def build_blog
  puts "Building this awesome Blog!"
  clean_older_files
  list_posts
  load_layout
  build_home
  build_posts
end

def load_layout
  layout = File.new("layouts/layout.html")
  @layout_page = layout.read.sub("{ blog_title }", @blog_title).sub("{ blog_logo }", @blog_logo)
  index = File.new("layouts/index.html")
  @index_page = index.read
  post = File.new("layouts/post.html")
  @post_page = post.read
end

def build_home
  index = File.new("../index.html", "w")
  list_of_posts = ""
  @posts.each do |post|
    list_of_posts += "<li><span class='muted'>#{post[:date]}</span> &raquo; <a href='#{post[:address]}'>#{post[:title]}</a></li>"
  end
  index.puts @layout_page.sub("{ yield }", @index_page.sub("{ yield }", list_of_posts))
  index.close
end

def build_posts
  @posts.each do |post|
    post_page = File.new("../#{post[:address]}", "w")
    post_page.puts @layout_page.sub("{ yield }", (@post_page.sub("{ title }", post[:title].to_s).sub("{ date }", post[:date].to_s).sub("{ text }", post[:text].to_s)))
    post_page.close
  end
end

def haiku
  #### Defining dates for new posts.
  @today = Time.now
  @year = @today.year
  @month = @today.month
  @day = @today.day
  @date = "#{@year}-#{fix_date(@month)}-#{fix_date(@day)}"
  @blog_title = "My Super Awesome Blog Name"
  @blog_logo = "~/Blog"
  #### end date code

  ask_user("Wanna make a new post?","y/n")
end
#### End Functions

haiku