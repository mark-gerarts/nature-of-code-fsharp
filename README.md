# Nature of Code - F\#

[Nature of Code](https://natureofcode.com/book/introduction/) ported to F# using
[Fable](https://fable.io/) and [Perfect Fifth](https://github.com/mark-gerarts/perfect-fifth).

## Development

One-time setup:

```sh
$ npm install
$ dotnet tool restore
$ dotnet restore src
```

Run a local development server on [localhost:8080](http://localhost:8080):

```sh
npm run start
```

## Website

To build the static website:

```sh
./generate-site.sh
```

Building & deploying is done automatically on commits to master.
