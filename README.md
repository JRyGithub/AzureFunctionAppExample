# AzureFunctionAppExample
An Azure Function App that parses data and can call a API and send respond correctly.

This a template C# project for an Azure Function App. Its purpose has been obfuscated but will fundamentally take in a request parsed to an Azure Function App and
then perform a number of operations on that data. In this case it will clean and validate the data, call a psedo API and pass the request. It will then clean the response and pass that back as another response to the azure logic app.
