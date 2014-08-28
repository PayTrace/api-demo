class ApplicationController < ActionController::Base
  # Prevent CSRF attacks by raising an exception.
  # For APIs, you may want to use :null_session instead.
  protect_from_forgery with: :exception
  
  def self.paytrace_api
    OAuth2::Client.new(
      nil, nil,
      site: 'https://api.paytrace.com',
    ).password.get_token(
      *Rails.configuration.paytrace_api_credentials
    )
  end
  
  protected
  
  def paytrace_api
    self.class.paytrace_api
  end
end
