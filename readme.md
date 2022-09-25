### Introduction

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
3. Navigate to `https://localhost:5000` in your browser of choice.
