# PartnerAuthDemo-csharp
A C# demo application for the OAUTH 2.0 authentication access code flow for authenticating with the partner API.

Documentation for the API can be found [here](https://api.ekm.net/v1/documentation).
The Partners site can be found [here](https://partners.ekm.net).

# Requirements
In order for partners to access the EKM API via OAuth2 all applications currently require a multi-step verification process.
1. You need to sign up for an account on the [Partners site](https://partners.ekm.net)
2. Please contact our Business Development team to have your account Verified.
3. Once verified, you can create an application and fill out the details.
4. This will allow you to 'Publish' the application. We don't currently have a test enviroment for API calls, so your application must be published in order to access the API. This will not make your application publicly visible on our platform.
5. The redirect uri of your application on the [Partners Site](https://partners.ekm.net) should match the uri that you are running the demo application from.
    - If you need this to work from multiple machines, you can create a fake URL using hostnames or some other method.
    - If you want to use a specific uri instead, you can [change this line of code](https://github.com/ekmsystems/PartnerAuthDemo-csharp/blob/master/Tempest.ExternalAuthDemoClient/Tempest.ExternalAuthDemoClient/Controllers/ConsentedController.cs#L29).
