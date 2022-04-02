#!/usr/bin/env sh

migration_name=$1

if [ -z "$migration_name" ]; then
  echo "provide name for migration" 
  exit 1
fi

dotnet ef migrations add "$migration_name" \
    --project ../BookChangelog.API.csproj  \
    --startup-project ../BookChangelog.API.csproj \
    --output-dir ./Infrastructure/Migrations