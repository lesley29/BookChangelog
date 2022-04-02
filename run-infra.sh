#!/bin/sh

docker compose down --remove-orphans -v
docker compose up -d start_dependencies