SagePay Direct Access Library
==

This is a C# library for providing SagePay Direct Integration to your .NET application.  
Currently, it is only scoped to handle requests and responses for non 3DAuth transactions.

This library has been built as a component of a larger application, and as such, 
is tailored to match that need.  As development on the main application progresses, 
revisions will be made to this library.  At this point, everything should be 
considered unstable, and liable to change at any time.

As if it goes without saying, neither Kian Ryan nor Orange Tentacle Ltd make any 
guarantees that this code will not cause the avoidable death of your loved ones, 
nor that it may still be a bit buggy.

License
--

Released under a MIT license - http://www.opensource.org/licenses/mit-license.php

Configuration
--

Add the following to your app.config/web.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<configSections>
		<section name="SagePay" type="OrangeTentacle.SagePay.Configuration.SageConfiguration, SagePay" />
	</configSections>

	<SagePay default="Live">
		<add type="Offline" vendorName="vendor_name" />
		<add type="Simulator" vendorName="vendor_name" />
		<add type="Test" vendorName="vendor_name" />
		<add type="Live" vendorName="vendor_name" />
	</SagePay>
</configuration>
```

Usage
--

After referencing the SagePay library.

To make a payment:


```csharp
var request = SagePayFactory.Payment.Fetch(); // Load default type
request = SagePayFactory.Payment.Fetch(ProviderTypes.Live); // Load live type

request.Transaction = new TransactionRequest(); // You may want to populate the transaction

request.Validate(); // Non validated transactions will throw on send.

var response = request.Send();
```

To make a refund:


```csharp
var request = SagePayFactory.Refund.Fetch(); // Load default type
request = SagePayFactory.Refund.Fetch(ProviderTypes.Live); // Load live type

request.Transaction = new RefundRequest(); // You may want to populate the transaction

request.Validate(); // Non validated transactions will throw on send.

var response = request.Send();
```


Todo
--
* Refund types incorporate "Refund" in to type, where Payment types are still bare.  This causes
  ambiguity.  Change payment type names to incorporate word "Payment".