# Add your own tasks in files placed in lib/tasks ending in .rake,
# for example lib/tasks/capistrano.rake, and they will automatically be available to Rake.

require 'rake/testtask'

Rake::TestTask.new do |t|
  t.libs << "lib"
  t.test_files = FileList['test/*_spec.rb']
  t.verbose = true
end
task :default => :test
