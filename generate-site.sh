#!/usr/bin/env sh
npm run build
rm -f gh-pages/output/*.html gh-pages/output/*.js gh-pages/output/*.css
cp public/bundle.js gh-pages/output/
cp public/style.css gh-pages/output/
dotnet fsi gh-pages/GenerateSite.fsx
