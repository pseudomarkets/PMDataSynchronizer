# PMDataSynchronizer
A .NET Standard library for synchronizing data across multiple instances of Pseudo Markets

# Requirements
* Two instances of Pseudo Markets running on two different SQL Server instances

# Usage
This library is used by the Pseudo Markets Unified API to trigger data sync during trades and initial account creation. To enable the data sync functionality, edit your appsettings.json file and set the "DataSyncEnabled" field to true and fill in the "DataSyncTargetDB" field with the connection string of the DB you'd like to synchronize changes to. For example:

App Server 1

| Primary DB | Data Sync Target DB |
|--|--|
| warhol\SQLExpress | raphael\SQLExpress |


App Server 2

| Primary DB | Data Sync Target DB |
|--|--|
| raphael\SQLExpress | warhol\SQLExpress |

With this configuration, both DBs will stay in sync for every insert and update that is made on either app server.

# NuGet
https://nuget.pseudomarkets.live/packages/PMDataSynchronizer

