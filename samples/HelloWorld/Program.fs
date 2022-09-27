module HelloWorld.Program

open Falco
open Falco.Markup
open Falco.Routing
open Falco.HostBuilder
open Falco.Htmx

module Components =
    let notClickedElement =
        Elem.div [ Attr.id "click_indicator" ] [
            Text.raw "Not clicked :-(" ]

    let clickMeElement =
        Elem.div [
            Attr.style "width: 40px; text-align: center; margin-bottom: 5px; border: 1px black solid;"
            Hx.post "/click"
            Hx.swap Hx.Swap.innerHTML
            Hx.target (Hx.Target.css "#click_indicator")
            (*Hx.trigger [ Hx.Triggers.click (Hx.Triggers.Modifiers.once) ] *) ] [
                Elem.div [
                    Attr.id "clicker"
                    Hx.target (Hx.Target.css "#clicker")
                    Hx.post "/change-to-reset-button"
                    (*Hx.trigger [Hx.Triggers.fromResponse (Hx.Triggers.ResponseEvent ("change-to-reset-button"))]*) ] [
                        Text.raw ("Click me!") ] ]

    let clickSection =
        Elem.div [ Attr.id "click-section" ] [
            clickMeElement
            notClickedElement ]

    let clickedElement =
        Elem.div [ Attr.id "click_indicator" ] [ Text.raw "Clicked!" ]

    let resetButton =
        Elem.div [
            Attr.id ("clicker")
            Hx.target (Hx.Target.css "#click-section")
            Hx.post "/reset"
            (* Hx.trigger [Hx.Triggers.click (Hx.Triggers.Modifiers.once)] *) ] [
                Text.raw ("Reset") ]

let handleHtml : HttpHandler =
    let html =
        Templates.html5 "en"
            [
                Elem.link [ Attr.href "style.css"; Attr.rel "stylesheet" ]
                Elem.script [ Attr.src "https://unpkg.com/htmx.org@1.8.0"] []
            ]
            [
                Elem.h1 [] [
                    Text.raw "Hello from Falco.Htmx" ]
                Components.clickSection
            ]

    Response.ofHtml html

let handleClick : HttpHandler =
    Response.withHxTrigger ("change-to-reset-button", None)
    >> Response.ofHtml Components.clickedElement


let handlerReset : HttpHandler =
    Response.ofHtml Components.clickSection

let handleChangeToResetButton : HttpHandler =
    Response.ofHtml Components.resetButton


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