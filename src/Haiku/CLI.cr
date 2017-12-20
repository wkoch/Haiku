# require "option_parser"

# folder = "HaikuWebsite"

# OptionParser.parse! do |parser|
#   parser.banner = "Usage: haiku [command] [folder]\n"
#   parser.on("-n folder", "--new=folder", "Creates a new project") { |value| folder = value }
#   parser.on("-b folder", "--build=folder", "Builds the project") { |value| folder = value }
#   parser.on("-h", "--help", "Show this help") { puts parser }
# end

# puts "#{folder}!"