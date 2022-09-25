namespace HelloWorld

open Falco.Markup
open Falco.Htmx
open Falco.Htmx.Helpers

[<RequireQualifiedAccess>]
module Components =
    let notClickedElement = Elem.div [Attr.id "click_indicator"] [Text.raw "Not clicked :-("]
    let clickMeElement = 
        Elem.div [
                Attr.style "width: 40px; text-align: center; margin-bottom: 5px; border: 1px black solid;";
                Hx.post (Uri "/click");
                Hx.target (CSS_Selector "#click_indicator");
                Hx.trigger [Hx.Triggers.click (Hx.Triggers.Modifiers.once)]
            ] 
            [
                Elem.div [Attr.id ("clicker"); Hx.target (CSS_Selector ("#clicker")); Hx.post (Uri "/change-to-reset-button"); Hx.trigger [Hx.Triggers.fromResponse (Hx.Triggers.ResponseEvent ("change-to-reset-button"))]] [
                    Text.raw ("Click me!")
                ]
            ]
    let clickSection = 
        Elem.div [Attr.id "click-section"] [
                    clickMeElement;
                    notClickedElement
                ]

    let clickedElement = Elem.div [Attr.id "click_indicator"] [Text.raw "Clicked!"]
    let resetButton = 
        Elem.div [
                Attr.id ("clicker");
                Hx.target (CSS_Selector ("#click-section"));
                Hx.post (Uri "/reset");
                Hx.trigger [Hx.Triggers.click (Hx.Triggers.Modifiers.once)]
            ]
            [
                Text.raw ("Reset")
            ]
