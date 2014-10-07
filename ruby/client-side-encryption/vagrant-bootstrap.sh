#!/bin/sh
apt-get update
apt-get install -y git nodejs rake ruby-bundler curl tmux 

\curl -sSL https://get.rvm.io | bash
usermod -a -G rvm vagrant

/usr/local/rvm/bin/rvm install 2.1.2

su - vagrant <<END_OF_COMMANDS
  cd /vagrant
  gem update bundler
  bundle install
END_OF_COMMANDS
