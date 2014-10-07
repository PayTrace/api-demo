package com.paytrace.hermes;

/**
 *
 * @author tomc
 */
public class CreditCard implements org.json.simple.JSONAware {
    public final String number;
    public final Integer expirationMonth;
    public final Integer expirationYear;
    
    public CreditCard(String number, int expirationMonth, int expirationYear) {
        this.number = number;
        this.expirationMonth = expirationMonth;
        this.expirationYear = expirationYear;
    }
    
    public String toJSONString() {
        return String.format("{\"number\": \"%s\", \"expiration_month\": %d, \"expiration_year\": %d}", number, expirationMonth, expirationYear);
    }
}
