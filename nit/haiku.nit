# haiku v0.4.0 05/2016 ~ William Koch
var version = "0.4.0"
var release_date = "05/2016"
var author = "William Koch"
var source = "https://gitlab.com/wkoch/haiku"

var help = """
	Haiku v{{{version}}} - {{{release_date}}}
	{{{author}}}
	{{{source}}}

	help: Shows this Help text.
	new: Creates a new Draft.
	build: Builds this blog.
	publish: Publishes your latest Draft.
	publish all: Publishes all Drafts."""

print help

fun ask_user(message,type="text") # Ask a questions, returns the answer.
  print message + " "
  gets
end

fun linkify(post_address) # Removes bad characters from the address string.
  bad_chars = {a: ["á", "à", "â", "ã", "ä"],
               e: ["é", "è", "ê", "ẽ", "ë"],
               i: ["í", "ì", "î", "ĩ", "ï"],
               o: ["ó", "ò", "ô", "õ", "ö"],
               u: ["ú", "ù", "û", "ũ", "ü"],
               c: ["ç"]}
  while post_address[/\W+/]
    bad_chars.each do |key, values|
      values.each do |char|
        post_address.gsub!(char, key)
      end
    end
  end
  post_address.gsub(/\s/, "-")
end
