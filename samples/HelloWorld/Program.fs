module HelloWorld.Program

open Falco
open Falco.Markup
open Falco.Routing
open Falco.HostBuilder
open Falco.Htmx
open HelloWorld

let useHtmxFromCdn = Elem.script [ Attr.src "https://unpkg.com/htmx.org@1.8.0"] []

let handleHtml : HttpHandler =
    let html =
        Templates.html5 "en"
            [ 
                Elem.link [ Attr.href "style.css"; Attr.rel "stylesheet" ];
                useHtmxFromCdn
            ]
            [ Elem.h1 [] [ 
                Text.raw "Hello from Falco.Htmx" ];
                Components.clickSection
            ]

    Response.ofHtml html

let handleClick : HttpHandler = fun ctx ->
    let triggerChangeToResetButton handler =
        ctx.SetHXTrigger("change-to-reset-button")
        handler ctx

    Components.clickedElement
    |> Response.ofHtml
    |> triggerChangeToResetButton
        

let handlerReset : HttpHandler =
    Components.clickSection
    |> Response.ofHtml

let handleChangeToResetButton : HttpHandler =
    Components.resetButton
    |> Response.ofHtml


[<EntryPoint>]
let main args =
    webHost args {
        endpoints [
            get "/" handleHtml
            post "/click" handleClick
            post "/change-to-reset-button" handleChangeToResetButton
            post "/reset" handlerReset
        ]
    }

    0 // Exit code