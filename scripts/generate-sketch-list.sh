#!/usr/bin/env bash

# Looks at the directory structure and generates the App.fsproj file based on
# that. Also updates the sketch list in App.fs.

SKETCH_FILES=$(find src -name *.fs \
    | grep src/[A-Z] \
    | sort \
    | tee >(head -n 1) | cat | tail -n +2)

# App.fsproj

COMPILE_LIST=$(echo "$SKETCH_FILES" \
    | sed 's/src\//    <Compile Include="/' \
    | sed 's/$/" \/>/')

REPLACEMENT=$(echo "<ItemGroup>\n$COMPILE_LIST\n  </ItemGroup>" | perl -pe 's/\//\\\//g')
UPDATED_FILE=$(cat src/App.fsproj | perl -0777 -pe "s/<ItemGroup>(\n|.)*?<\/ItemGroup>/$REPLACEMENT/s")
echo "$UPDATED_FILE" > src/App.fsproj

# App.fs

SKETCH_LIST=$(echo "$SKETCH_FILES" \
    | grep -v "src/App.fs" \
    | while IFS= read -r line; do \
        echo "$line" | sed -E 's#^src/([^/]+)/([^/]+)/(.*)\.fs$#"\1/\2/\3", \1.\2.\3.run#' ; \
    done \
    | sed '$s/$/ ]/' \
    | sed 's/^/      /' \
    | sed '1 s/      /[ /')

REPLACEMENT=$(echo "$SKETCH_LIST" | perl -pe 's/\//\\\//g')
UPDATED_FILE=$(cat src/App.fs | perl -0777 -pe "s/\[ (\n|.)*? \]/$REPLACEMENT/s")
echo "$UPDATED_FILE" > src/App.fs
