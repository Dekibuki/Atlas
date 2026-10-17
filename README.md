# Atlas 

## How-to Guide

## For Discussion

* Odd-named variable for application request - The name of the "country" variable does not reflect it's full meaning. Should discuss with mobile team for a better name, as it represents a market more rather than a country.
* Lack of encryption - API handles sensitive personal data such as ID, passport, phone, email, etc. Discuss which data should be encrypted for an added layer of protection.
* Lack of validation - There's no validation of the user's request. Validation might be done in another part of the system (frontend, perhaps?), however it is better to have validation in the API as well. Will add basic validation - discuss further on whether it should remain/be changed/be removed.
* Authorization for service calls - unknown what kind of authorization the IDNow and World-Check APIs use. Bearer? Secret + ClientID? Anonymous allowed? Needs to be discussed during review.

