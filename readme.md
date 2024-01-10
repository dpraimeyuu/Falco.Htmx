# Falco.Htmx

[![NuGet Version](https://img.shields.io/nuget/v/Falco.Htmx.svg)](https://www.nuget.org/packages/Falco.Htmx)
[![build](https://github.com/dpraimeyuu/Falco.Htmx/actions/workflows/build.yml/badge.svg)](https://github.com/dpraimeyuu/Falco.Htmx/actions/workflows/build.yml)

<!--
## Key Features

> TODO

## Design Goals

> TODO

## Learn

> TODO
-->

## Getting Started

An experimental [Falco](https://github.com/pimbrouwers/Falco) integration with [htmx](https://htmx.org) JS package.

![falco-htmx-teaser](https://user-images.githubusercontent.com/6437191/192160689-6fc059d2-55a6-4d75-b8e3-7c78de3c2a91.gif)

`Falco.Htmx` adds new attribute - `Hx` - that brings type-safe attributes of `htmx` to the [Falco](https://github.com/pimbrouwers/Falco) world, for example:

```fsharp
let resetButton =
        Elem.div [
                Attr.id ("clicker");
                Hx.target (CSS_Selector ("#click-section")); // new!
                Hx.post (Uri "/reset"); // new!
                Hx.trigger [Hx.Triggers.click (Hx.Triggers.Modifiers.once)] // new!
            ]
            [ Text.raw ("Reset") ]
```

### Curious?

If you want to check how it looks and feels:
1. Go to `/samples/HelloWorld`.
2. Use `dotnet run` command in your favorite terminal.
3. Navigate to `http://localhost:5000` in your browser of choice.

## Kudos

Big thanks and huuuge kudos to [@pim_brouwers](https://twitter.com/pim_brouwers) and to [@angel_d_munoz](https://twitter.com/angel_d_munoz) for help, inspiration and collaboration 🚀

## Find a bug?

There's an [issue](https://github.com/dpraimeyuu/Falco.Htmx/issues) for that.

## Developing and testing Github Actions locally:
1. Install [act](https://github.com/nektos/act)
2. Run in the main directory of the package:
```shell
act release -s NUGET_APIKEY=<<ANY_API_KEY>> --artifact-server-path /tmp/artifacts
```
where:
* `<<ANY_API_KEY>>` - put here anything as `act` will run it locally and we don't want to publish anything to nuget

## License

Built with ♥ by [Damian Plaza](https://github.com/dpraimeyuu) & [Pim Brouwers](https://github.com/pimbrouwers). Licensed under [Apache License 2.0](https://github.com/dpraimeyuu/Falco.Htmx/blob/master/LICENSE).
