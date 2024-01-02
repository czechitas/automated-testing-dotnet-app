#!/bin/bash

set -ex

dotnet publish AutomatedTestingApp/AutomatedTestingApp/AutomatedTestingApp.csproj -c Release -o out /p:UseAppHost=false

docker build --build-arg APP_PATH=./out -t hiresm/czechitas:test .