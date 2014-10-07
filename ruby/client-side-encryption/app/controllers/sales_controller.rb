class SalesController < ApplicationController
  def new
  end

  def new_encrypted
  end

  def create
    @request = params[:sale]
    @response = paytrace_connection.post('/v1/transactions/sale/keyed', body: @request)
  end
end