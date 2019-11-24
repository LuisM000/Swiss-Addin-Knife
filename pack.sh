#!/bin/bash

nuget restore SwissAddinKnife.sln

msbuild /p:Configuration=Release SwissAddinKnife.sln /p:OutDir=./bin/Release/pack  /p:CreatePackage=true

cp SwissAddinKnife/bin/Release/pack/SwissAddinKnife.*.mpack .