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
		<section name="OfflineSagePay" type="OrangeTentacle.SagePay.SageConfiguration, SagePay" />
		<section name="SimulatorSagePay" type="OrangeTentacle.SagePay.SageConfiguration, SagePay" />
		<section name="TestSagePay" type="OrangeTentacle.SagePay.SageConfiguration, SagePay" />
		<section name="LiveSagePay" type="OrangeTentacle.SagePay.SageConfiguration, SagePay" />
	</configSections>

	<OfflineSagePay vendorName="vendor_name" />
	<SimulatorSagePay vendorName="vendor_name" />
	<TestSagePay vendorName="vendor_name" />
	<LiveSagePay vendorName="vendor_name" />
</configuration>
```

Usage
--

After referencing the SagePay library:

```csharp
var  request = new SagePay.SimulatorSageRequest();
request.Transaction = new TransactionRequest(); // You may want to populate the transaction

request.Validate(); // Non validated transactions will throw on send.

var response = request.Send();
```


Todo
--
* Write a factory which will return an instance of WebSageRequest dependant
  on the configuration file (some form of default provider-type).
* Implement refunds.