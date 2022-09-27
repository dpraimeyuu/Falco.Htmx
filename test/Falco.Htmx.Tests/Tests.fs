namespace Falco.Htmx.Tests

open System
open Falco.Htmx
open Falco.Markup
open Falco.Htmx
open FsUnit.Xunit
open Xunit

[<AutoOpen>]
module Common =
    let elem attr = Elem.div attr [ Text.raw "div" ]

module Extensions =
    [<Fact>]
    let ``Hx.get should produce element with hx-get attribute`` () =
        elem [ Hx.get "/" ]
        |> renderNode
        |> should equal "<div hx-get=\"/\">div</div>"

    [<Fact>]
    let ``Hx.post should produce element with hx-post attribute`` () =
        elem [ Hx.post "/" ]
        |> renderNode
        |> should equal "<div hx-post=\"/\">div</div>"

    [<Fact>]
    let ``Hx.put should produce element with hx-put attribute`` () =
        elem [ Hx.put "/" ]
        |> renderNode
        |> should equal "<div hx-put=\"/\">div</div>"

    [<Fact>]
    let ``Hx.patch should produce element with hx-patch attribute`` () =
        elem [ Hx.patch "/" ]
        |> renderNode
        |> should equal "<div hx-patch=\"/\">div</div>"

    [<Fact>]
    let ``Hx.delete should produce element with hx-delete attribute`` () =
        elem [ Hx.delete "/" ]
        |> renderNode
        |> should equal "<div hx-delete=\"/\">div</div>"

// module ``Htmx`` =
//     let withHtmlTag htmlTree=
//         "<!DOCTYPE html>" + htmlTree

//     [<Fact>]
//     let ``renders div with "hx-get" attribute containing given url`` () =
//         let element =
//             Elem.div [Hx.get (Uri "/posts")] []
//             |> renderHtml
//         let expected =
//             withHtmlTag "<div hx-get=\"/posts\"></div>"

//         Assert.Equal(expected, element)
//     [<Fact>]
//     let ``renders div with "hx-post" attribute containing given url`` () =
//         let element =
//             Elem.div [Hx.post (Uri "/posts")] []
//             |> renderHtml
//         let expected =
//             withHtmlTag "<div hx-post=\"/posts\"></div>"

//         Assert.Equal(expected, element)

//     [<Fact>]
//     let ``renders div with "hx-put" attribute containing given url`` () =
//         let element =
//             Elem.div [Hx.put (Uri "/posts")] []
//             |> renderHtml
//         let expected =
//             withHtmlTag "<div hx-put=\"/posts\"></div>"

//         Assert.Equal(expected, element)

//     [<Fact>]
//     let ``renders div with "hx-delete" attribute containing given url`` () =
//         let element =
//             Elem.div [Hx.delete (Uri "/posts")] []
//             |> renderHtml
//         let expected =
//             withHtmlTag "<div hx-delete=\"/posts\"></div>"

//         Assert.Equal(expected, element)


//     [<Fact>]
//     let ``renders div with "hx-patch" attribute containing given url`` () =
//         let element =
//             Elem.div [Hx.patch (Uri "/posts")] []
//             |> renderHtml
//         let expected =
//             withHtmlTag "<div hx-patch=\"/posts\"></div>"

//         Assert.Equal(expected, element)

//     module ``Triggers`` =

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter every 2s" trigger`` () =
//             let mouseenterEvery2Seconds' =
//                 mouseenter (every 2.<second>)
//             let element =
//                 Elem.div [Hx.trigger [mouseenterEvery2Seconds']] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter every 2s\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter once" trigger`` () =
//             let mouseenterOnce' =
//                 mouseenter Modifiers.once
//             let element =
//                 Elem.div [Hx.trigger [mouseenterOnce']] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter once\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter delay:0.5s" trigger`` () =
//             let mouseenterDelayHalfSecond =
//                 mouseenter (delay 0.5<second>)
//             let element =
//                 Elem.div [Hx.trigger [mouseenterDelayHalfSecond]] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter delay:0.5s\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter throttle:0.7s" trigger`` () =
//             let mouseenterThrottle700MiliSeconds =
//                 mouseenter (throttle 0.7<second>)
//             let element =
//                 Elem.div [Hx.trigger [mouseenterThrottle700MiliSeconds]] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter throttle:0.7s\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter target:#nameField" trigger`` () =
//             let mouseenterTargetIdNameField =
//                 mouseenter (target (CSS_Selector "#nameField"))
//             let element =
//                 Elem.div [Hx.trigger [mouseenterTargetIdNameField]] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter target:#nameField\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter consume" trigger`` () =
//             let mouseenterConsume =
//                 mouseenter consume
//             let element =
//                 Elem.div [Hx.trigger [mouseenterConsume]] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter consume\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing "mouseenter queue:none" trigger`` () =
//             let mouseenterQueryNone =
//                 mouseenter (queue Queue.None)

//             let element =
//                 Elem.div [Hx.trigger [mouseenterQueryNone]] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"mouseenter queue:none\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-trigger" attribute containing custom event "doX" trigger`` () =

//             let element =
//                 Elem.div [Hx.trigger [fromResponse (ResponseEvent "doX")]] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-trigger=\"doX from:body\"></div>"

//             Assert.Equal(expected, element)

//     module ``Swap`` =
//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "innerHTML" swap`` () =

//             let element =
//                 Elem.div [Hx.swap innerHTML] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"innerHTML\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "outerHTML" swap`` () =

//             let element =
//                 Elem.div [Hx.swap outerHTML] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"outerHTML\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "beforebegin" swap`` () =

//             let element =
//                 Elem.div [Hx.swap beforebegin] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"beforebegin\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "afterbegin" swap`` () =

//             let element =
//                 Elem.div [Hx.swap afterbegin] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"afterbegin\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "beforeend" swap`` () =

//             let element =
//                 Elem.div [Hx.swap beforeend] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"beforeend\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "delete" swap`` () =

//             let element =
//                 Elem.div [Hx.swap delete] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"delete\"></div>"

//             Assert.Equal(expected, element)

//         [<Fact>]
//         let ``renders div with "hx-swap" attribute containing "none" swap`` () =

//             let element =
//                 Elem.div [Hx.swap none] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-swap=\"none\"></div>"

//             Assert.Equal(expected, element)

//     module ``Target`` =

//         [<Fact>]
//         let ``renders div with "hx-target" attribute containing "#nameField" target`` () =
//             let element =
//                 Elem.div [Hx.target (CSS_Selector("#nameField"))] []
//                 |> renderHtml
//             let expected =
//                 withHtmlTag "<div hx-target=\"#nameField\"></div>"

//             Assert.Equal(expected, element)