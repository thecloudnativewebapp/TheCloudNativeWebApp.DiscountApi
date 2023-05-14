Feature: Viewing Vouchers
    In order to see which vouchers I issued
    As a Customer Loyalty employee
    I want to see the vouchers in the system

Scenario: Vouchers
Given the system contains vouchers
 When I GET the /api/voucher endpoint
 Then the API responds with an status code 200 (OK)
  And I get a list of vouchers
 
Scenario: No vouchers
Given the system does contain vouchers
 When I GET the /api/voucher endpoint
 Then the API responds with an status code 200 (OK)
  And I get an empty list