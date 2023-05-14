Feature: Creating Vouchers
	In order to reward customers for their loyalty
	As a business
	I want to issue vouchers to loyal customers
	  
Scenario Outline: New voucher
	Given a <vouchercode> and a <value>
	 When the request is posted to the /api/voucher endpoint
	 Then the API responds with an status code 200 (OK)
	  And the voucher has been saved
	  And the voucher contains the vouchercode and the value
Examples:
| vouchercode   | value     |
| 00000001      | 0.01      |
| 00000001      | 1         |
| 00000001      | 10000000  |

Scenario Outline: Incorrect value
	Given a <vouchercode> and a <value>
	 When the request is posted to the /api/voucher endpoint
	 Then the API responds with an status code 400 (Bad Request)
	  And the application produces an error which implies the value is incorrect
Examples:
| vouchercode   | value |
| 00000001      | 0     |
| 00000001      | 1.123 |
| 00000001      | -1    |
| 00000001      | -0.1  |
| 00000001      | text  |
| 00000001      |       |

Scenario Outline: Incorrect vouchercode
	Given a <vouchercode> and a <value>
	 When the request is posted to the /api/voucher endpoint
	 Then the API responds with an status code 400 (Bad Request)
	  And the application produces an error which implies the vouchercode is incorrect
Examples:
| vouchercode   | value |
| sometext      | 1     |
| 1234567       | 1     |
| 123456789     | 1     |
|               | 1     |

Scenario Outline: Existing vouchercode
	Given a <vouchercode> and a <value>
	  And a voucher has been created in the past with vouchercode 00000001
	 When the request is posted to the /api/voucher endpoint
	 Then the API responds with an status code 409 (Conflict)
Examples:
| vouchercode   | value |
| 00000001      | 1     |