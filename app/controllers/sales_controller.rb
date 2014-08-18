class SalesController < ApplicationController
  def new
  end

  def new_encrypted
  end

  def create
    backend_host = Rails.configuration.paytrace_api_server
    client = OAuth2::Client.new(nil, nil, :site => backend_host, ssl: {verify: false})
    token = client.password.get_token(*Rails.configuration.paytrace_api_credentials)

    @request = params[:sale]
    @response = token.post('/v1/transactions/sale/keyed', body: @request)
  end
end