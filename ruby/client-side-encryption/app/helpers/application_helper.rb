module ApplicationHelper
  Host_site = 'http://apitest2.paytrace.com'
  User = 'demo123'
  Pass = 'demo123'
  def backend_site_keyed_sales
      Host_site + '/v1/transactions/sale/keyed.json'
  end
  def token
      'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA+PDBeoBQJzR806H+eYze8VSa3CWBmBpoGxa+WQ8carTju/vtfxdNqc2VSU5p73KREdNiqTdJVssnwiu7ttxDtvl0WM1lNbAL2Iz3EH+1ZKtsvzPCQwhQ5qh8G6+x5kJ2Z04VIEP6rlzteB9HdaRcg21P8KYbGa+MTn4cLpAJuGKnfMgJHGWJJ1W5ENhMhIC6f8bjjGhaDbyM4ARMDpiZPVe5ENpQt09JnAZLbC6lg5E1WGGMvNRS2FkNCQnppHajohaEXPFu6ByXKxDw3K9VThNl4FYFiE1kyrrJaA1hnX/cpUWNORB995d2BAXlxtrrvaSmVPX7XpOuYCXRMRcVhwIDAQAB'
      #client = OAuth2::Client.new(nil, nil, :site => Host_site, ssl: {verify: false})

      #client.password.get_token(User, Pass)
      #token.get('/v1/public_key')
      #response = token.get('/v1/ping')
      # response = token.post('/v1/transactions/sale/keyed.json', {body:
    	# {
    	# 	:amount => "123",
    	# 	:credit_card => { :number => '4921812930953704', :expiration_month => '12', :expiration_year => '2014' }}
    	# })
  end
end
