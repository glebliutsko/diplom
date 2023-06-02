#!/bin/sh

DOTNET_PUBLISH_OPTION="-c Release -p:PublishReadyToRun=true -f net7.0"
DOTNET_PUBLISH_OPTION_SELF="$DOTNET_PUBLISH_OPTION -p:IncludeNativeLibrariesForSelfExtract=true --self-contained"
DOTNET_PUBLISH_OPTION_NO_SELF="$DOTNET_PUBLISH_OPTION --no-self-contained"

BASEDIR=$(realpath $(dirname $0))
BUILDDIR="$BASEDIR/build"

PATH_TO_PROJECT_SERVER="$BASEDIR/src/ExamPapers.ServerAPI"

cd $BASEDIR

# Windows (Server)
dotnet publish $PATH_TO_PROJECT_SERVER $DOTNET_PUBLISH_OPTION_SELF -r win-x64 -o "$BUILDDIR/windows-self"
dotnet publish $PATH_TO_PROJECT_SERVER $DOTNET_PUBLISH_OPTION_NO_SELF -r win-x64 -o "$BUILDDIR/windows-no-self"

# Linux (Server)
dotnet publish $PATH_TO_PROJECT_SERVER $DOTNET_PUBLISH_OPTION_SELF -r linux-x64 -o "$BUILDDIR/linux-self"
dotnet publish $PATH_TO_PROJECT_SERVER $DOTNET_PUBLISH_OPTION_NO_SELF -r linux-x64 -o "$BUILDDIR/linux-no-self"
