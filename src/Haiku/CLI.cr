require "option_parser"

destination = "World"

OptionParser.parse! do |parser|
  parser.banner = "Usage: haiku [command] [folder]\n"
  parser.on("-n folder", "--new=folder", "Creates a new project") { |folder| destination = folder || nil }
  parser.on("-b folder", "--build=folder", "Builds the project") { |folder| destination = folder || nil }
  parser.on("-h", "--help", "Show this help") { puts parser }
end

puts "Hello #{destination}!"