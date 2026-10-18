# Atlas 
This document will contain a guide on how to use the API, as well as a bullet list with all points of discussion. 

## How-to Guide

## For Discussion

* Odd-named variable for application request - The name of the "country" variable does not reflect it's full meaning. Should discuss with mobile team for a better name, as it represents a market more rather than a country.
* Lack of encryption - API handles sensitive personal data such as ID, passport, phone, email, etc. Discuss which data should be encrypted for an added layer of protection.
* Lack of validation - There's no validation of the user's request. Validation might be done in another part of the system (frontend, perhaps?), however it is better to have validation in the API as well. Will add basic validation - discuss further on whether it should remain/be changed/be removed.
* Authorization for service calls - "Credentials are in the vault" - not specified which storage vault they are in. Or how to access the vault.
* Not sure what to do with errors - Since there's to be no human intervention, when the system encounters an error or one of the calls to IDNow or WorldCheck fail, the user will receive a REJECTED result instead of an error popup for now.
* Retries - requirements specify to attempt calling third-party APIs with retries if they are down. Does not specify if there should be a delay or how many re-tries to attempt.
* Account and Card - unknown what fields/information should be stored into these 2 items. Approximating - but should be discussed.
