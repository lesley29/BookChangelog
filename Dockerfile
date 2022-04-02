FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env

WORKDIR /sln

COPY ./BookChangelog.API/BookChangelog.API.csproj \
    ./BookChangelog.sln \
    ./

RUN for file in *.csproj; do \
        filename=$(basename $file) && \
        dirname=${filename%.*} && \
        mkdir $dirname && \
        mv $filename ./$dirname/; \
    done

RUN dotnet restore BookChangelog.API.sln

COPY ./ ./

RUN dotnet build ./BookChangelog.API.sln -c Release --no-restore

RUN dotnet publish ./BookChangelog.API/BookChangelog.API.csproj \
    -o ./published/BookChangelog.API \
    -c Release \
    --no-build

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /sln
COPY --from=build-env ./sln/published ./published

ENTRYPOINT ["./BookChangelog.API"]