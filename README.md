# Atlas 
This document will contain a guide on how to use the API, as well as a bullet list with all points of discussion. 

## How-to Guide
The API can be tested using either a Postman-like HTTP caller or through it's SwaggerUI. The API is hosted on **https://localhost:5006** - while the SwaggerUI is hosted on **https://localhost:5006/index.html**.

There are 3 GET endpoints:
* atlas/applications - gets a list of all applications
* atlas/accounts - gets a list of all accounts
* atlas/cards - gets a list of all cards
All 3 GET endpoints can be called with no parameters, only the correct route.

There is one POST endpoint - **atlas/applications**. It receives a JSON Body similar to this:
```
{
  "firstName": "Aleksandar",
  "lastName": "Bocevski",
  "dateOfBirth": "2000-09-19T06:44:11.534Z",
  "country": 2,
  "nationalId": "1909000450016",
  "email": "alexandar.bocevski@gmail.com",
  "phone": "070296904",
  "documents": [
    {
      "type": 0,
      "image": "SGVsbG8gV29ybGQh"
    },
    {
      "type": 2,
      "image": "SGVsbG8gV29ybGQh"
    }
  ],
  "termsAccepted": true
}
```

## For Discussion

* Odd-named variable for application request - The name of the "country" variable does not reflect it's full meaning. Should discuss with mobile team for a better name, as it represents a market more rather than a country.
* Lack of encryption - API handles sensitive personal data such as ID, passport, phone, email, etc. Discuss which data should be encrypted for an added layer of protection.
* Lack of validation - There's no validation of the user's request. Validation might be done in another part of the system (frontend, perhaps?), however it is better to have validation in the API as well. Will add basic validation - discuss further on whether it should remain/be changed/be removed.
* Authorization for service calls - "Credentials are in the vault" - not specified which storage vault they are in. Or how to access the vault.
* Not sure what to do with errors - Since there's to be no human intervention, when the system encounters an error or one of the calls to IDNow or WorldCheck fail, the user will receive a REJECTED result instead of an error popup for now.
* Retries - requirements specify to attempt calling third-party APIs with retries if they are down. Does not specify if there should be a delay or how many re-tries to attempt.
* Account and Card - unknown what fields/information should be stored into these 2 items. Approximating - but should be discussed.
* Auto-reject - System currently has to auto reject if an application takes too long. The discussion board mentions that there might be a situation where a World-Check screening might go into a manual queue - that could last 48 hours. This needs to be discussed on how to go forward.
