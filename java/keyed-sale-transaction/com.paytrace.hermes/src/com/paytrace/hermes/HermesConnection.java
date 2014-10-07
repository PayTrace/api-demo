package com.paytrace.hermes;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.security.cert.X509Certificate;
import javax.net.ssl.SSLContext;
import javax.net.ssl.TrustManager;
import javax.net.ssl.X509TrustManager;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
/**
 *
 * @author tomc
 */

public class HermesConnection {
    private final String resourceServerUrl;
    private final String authenticationServerUrl;
    private final String username;
    private final String password;
    private String token = null;

    public HermesConnection(String username, String password) {
        this(username, password, "https://api.paytrace.com", "https://api.paytrace.com");
    }
    
    public HermesConnection(String username, String password, String authServer, String resourceServer) {
        this.username = username;
        this.password = password;
        this.authenticationServerUrl = authServer;
        this.resourceServerUrl = resourceServer;
    }

    public String getToken() throws Exception {
        if (token == null) {

            URL fullUrl = new URL(authenticationServerUrl + "/oauth/token");
            HttpURLConnection con = (HttpURLConnection) fullUrl.openConnection();
            con.setRequestMethod("POST");
            
            con.setDoOutput(true);
            con.setRequestProperty("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            try (OutputStream output = con.getOutputStream()) {
                String query = "grant_type=password&username=" + username + "&password=" + password;

                output.write(query.getBytes("UTF-8"));
            }

            InputStream is = con.getInputStream();
            StringBuilder sb = new StringBuilder();
            int ch;

            while ((ch = is.read()) != -1) {
                sb.append((char) ch);
            }

            JSONObject obj = parseJson(sb.toString());
            token = (String)obj.get("access_token");
        }
        
        return token;
    }

    // never, ever, ever, ever, ever use this type of code in production! it turns off ALL SSL security!
    // only use it when connecting to test servers with invalid SSL certs, like apitest2.paytrace.com
    public void enableTest() throws Exception {        
        try {
            X509TrustManager tm = new X509TrustManager() {
                @Override
                public X509Certificate[] getAcceptedIssuers() {
                    return null;
                }

                @Override
                public void checkServerTrusted(X509Certificate[] paramArrayOfX509Certificate, String paramString) {

                }

                @Override
                public void checkClientTrusted(X509Certificate[] paramArrayOfX509Certificate, String paramString) {

                }
            };
                    
            SSLContext ctx = SSLContext.getInstance("TLS");
            ctx.init(null, new TrustManager[] { tm }, null);
            SSLContext.setDefault(ctx);  
            
            javax.net.ssl.HttpsURLConnection.setDefaultHostnameVerifier(
                new javax.net.ssl.HostnameVerifier(){ 
                    public boolean verify(String hostname, javax.net.ssl.SSLSession sslSession) {
                        return true;
                    }
                }
            );
        } catch(Exception ex) {
            throw ex;
        }
    }
    
    public JSONObject call(String fullPath, JSONObject data) throws Exception {
        return call(fullPath, data, "POST");
    }
    
    public JSONObject call(String fullPath, JSONObject data, String requestMethod) throws Exception {
        URL fullUrl = new URL(resourceServerUrl + fullPath);
        
        HttpURLConnection con = (HttpURLConnection) fullUrl.openConnection();
                                con.setDoOutput(true); // Triggers POST.

        con.setRequestMethod(requestMethod);
        con.setRequestProperty("Accept", "application/json;charset=UTF-8");
        con.setRequestProperty("Authorization", "Bearer " + getToken());

        if (requestMethod.equals("POST")) {
           con.setRequestProperty("Content-Type", "application/json;charset=UTF-8");

            OutputStream output = con.getOutputStream();

            if (data != null) {
                output.write(data.toString().getBytes("UTF-8"));
            }
        }
        
        InputStream is = con.getInputStream();
        StringBuilder sb = new StringBuilder();
        int ch;

        while ((ch = is.read()) != -1) {
            sb.append((char) ch);
        }

        return parseJson(sb.toString());
    }
    
    private JSONObject parseJson(String raw) throws ParseException {
        JSONParser parser=new JSONParser();
        return (JSONObject)parser.parse(raw);
    }
}