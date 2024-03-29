# EZClaimAPIHelper

This project serves as a valuable tool for interacting with the EZClaim API. If you are interested in acquiring a testing company file, please fill out [this form](https://share.hsforms.com/1oBYQ3HeMRjGf-hk1WaUxCw8nkmb).

For instructions on generating tokens from within the EZClaim application, as well as a detailed list of all endpoints, valid data for those endpoints, and an explanation of the various OData types, please refer to [this Knowledge Base article](https://kb.ezclaim.com/knowledge/ezclaim-api).

Swagger documentation can be found [here](https://ezclaimapidev.azurewebsites.net/swagger/index.html).

## Where to begin

Before looking at all of the examples that exist in this project, we recommend starting by looking at the examples in the [FullCrudPatient_UT](EZClaimAPIHelper.UT/Realistic/FullCrudPatient_UT.cs) This will give very simple examples of how to interact with the Patient endpoints in the EZClaimAPI.

NOTE how in our examples we are generating a new AES object every time. You don't have to do this, but there is no need to store your AES settings anywhere, so we'd recommended doing this for ease of use.


## [APIUnitTestHelperObject](EZClaimAPIHelper.UT/APIUnitTestHelperObject.cs)
The APIUnitTestHelperObject is what is used for all other test classes. This class is used for setting up the request that is sent to the api and reading the result that is returned from the api

## [AESHelper](EZClaimAPIHelper/AESHelper.cs)

Treat this file as an example for using AES to encrypt and decrypt as supplied string

## [RSAHelper](EZClaimAPIHelper/RSAHelper.cs)

Treat this file as an example for using RSA to encrypt and decrypt as supplied AES key

## [APIHelper](EZClaimAPIHelper/APIHelper.cs)

Treat this file as an example for talking to the EZClamAPI using the supplied headers and encrypted body

## [Realistic examples](EZClaimAPIHelper.UT/Realistic)

In the Realistic folder, you will see realistic examples for all controller types that exist in the api.

These files each contain examples for their respective api endpoints.

## [ExamplesOfBadOdata](EZClaimAPIHelper.UT/Odata/ExamplesOfBadOdata)

The ExamplesOfBadOdata contains examples of different errors that you might get when you make a call to an endpoint using bad odata.

## [ExamplesOfGoodOdata](EZClaimAPIHelper.UT/Odata/ExamplesOfGoodOdata)

The ExamplesOfGoodOdata contains examples of different calls you could make to the api using correctly formatted odata

## [OtherExamples](EZClaimAPIHelper.UT/OtherExamples)

The OtherExamples folder contains examples of how you would set a column to null along with examples of what happens if you attempt to set an auto column

## [BadParameters_UT](EZClaimAPIHelper.UT/BadParameters_UT.cs)

The BadParameters_UT file contains examples of some errors that you can get if you pass in a bad parameter or forget to pass in a specific header

## [RSAGenerator_UT](EZClaimAPIHelper.UT/RSAGenerator_UT.cs)

The RSAGenerator_UT file shows how we generated our rsa key AND gives help for converting that generated rsa key to an RSA file.

## [OtherErrors](EZClaimAPIHelper.UT/OtherErrors)

The other errors folder gives examples of some of the other types of common errors that you can encounter while calling the EZClaimAPI

## Quick note

At the time of writing this (2023/05/11) the api is constantly being updated. If you don't have an endpoint that you want or need OR if we don't have an odata type that you want or need OR if you have a suggestion, please don't hesitate to let us know.
