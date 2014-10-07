package com.paytrace.hermes.sample;

import com.paytrace.hermes.CreditCard;
import com.paytrace.hermes.HermesConnection;
import org.json.simple.JSONObject;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 *
 * @author tomc
 */
public class HermesSample {
    /**
     * @param args the command line arguments
     * @throws Exception
     */
    public static void main(String[] args) throws Exception {
        HermesConnection connection = new HermesConnection(
                "demo123", // username; get from config in production
                "demo123", // password; get from config in production
                "https://apitest2.paytrace.com", // authentication server; omit this param in production
                "https://apitest2.paytrace.com" // resource server; omit this param in production
        );
        
        // never, ever, ever do this in production!
        // this disables ALL SSL verification, leaving you vulnerable to MITM and other attacks!
        // it's only necessary for the self-signed cert on apitest2
        connection.enableTest(); 

        // we're using the json.simple library; see https://code.google.com/p/json-simple/
        JSONObject create_data = new JSONObject();
        create_data.put("amount", 19.00);
        create_data.put("credit_card", new CreditCard("4111111111111111", 12, 20));

        System.out.println("REQUEST :\n" + create_data.toString());
        System.out.println("RESPONSE:\n" + connection.call("/v1/transactions/sale/keyed", create_data, "POST"));
    }
}
