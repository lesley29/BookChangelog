#!/usr/bin/env sh

dotnet ef database update \
    --project ../BookChangelog.API.csproj  \
    --startup-project ../BookChangelog.API.csproj