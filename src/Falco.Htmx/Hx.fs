[<RequireQualifiedAccess>]
module Falco.Htmx.Hx

open System
open System.Text.Json
open Falco.Markup
open FSharp.Core

module Target =
    let this = This
    let css selector = CssSelector selector
    let closest selector = Closest selector
    let find selector = Find selector

module Timing =
    let ms (milliseconds : float) = Milliseconds milliseconds
    let s (seconds : float) = Seconds seconds
    let minutes (minutes : float) = Minutes minutes

module Trigger =
    let event (name : string, filters : string option, modifiers : EventModifier list) =
        Event (name, filters, modifiers)

    let poll (timing : TimingDeclaration) = Poll timing

module Swap =
    let innerHTML = InnerHTML
    let outerHTML = OuterHTML
    let beforebegin = BeforeBegin
    let afterbegin = AfterBegin
    let beforeend = BeforEend
    let afterend = AfterEnd
    let delete = Delete
    let none = NoSwap

module SwapOob =
    let true' = SwapTrue

    let innerHTML = SwapOption InnerHTML
    let outerHTML = SwapOption OuterHTML
    let beforebegin = SwapOption BeforeBegin
    let afterbegin = SwapOption AfterBegin
    let beforeend = SwapOption BeforEend
    let afterend = SwapOption AfterEnd
    let delete = SwapOption Delete
    let none = SwapOption NoSwap

    let innerHTMLTarget target = SwapOptionSelect (InnerHTML, target)
    let outerHTMLTarget target = SwapOptionSelect (OuterHTML, target)
    let beforebeginTarget target = SwapOptionSelect (BeforeBegin, target)
    let afterbeginTarget target = SwapOptionSelect (AfterBegin, target)
    let beforeendTarget target = SwapOptionSelect (BeforEend, target)
    let afterendTarget target = SwapOptionSelect (AfterEnd, target)
    let deleteTarget target = SwapOptionSelect (Delete, target)
    let noneTarget target = SwapOptionSelect (NoSwap, target)

module Sync =
    let drop = Drop
    let abort = Abort
    let replace = Replace
    let queueFirst = Queue First
    let queueLast = Queue Last
    let queueAll = Queue All

module Url =
    let true' = True
    let false' = False
    let path url' = Url url'

module Param =
    let all = AllParam
    let none = NoParams
    let exclude names = ExcludeParam names
    let include' names = IncludeParam names

module Disinherit =
    let all = AllAttributes
    let exclude names = ExcludeAttributes names

// ------------
// AJAX
// ------------

/// Issues a GET request to the given URL
let get (uri : string) = Attr.create "hx-get" uri

/// Issues a POST request to the given URL
let post (uri : string) = Attr.create "hx-post" uri

/// Issues a PUT request to the given URL
let put (uri : string) = Attr.create "hx-put" uri

/// Issues a PATCH request to the given URL
let patch (uri : string) = Attr.create "hx-patch" uri

/// Issues a DELETE request to the given URL
let delete (uri : string) = Attr.create "hx-delete" uri

// ------------
// Commmon Attributes
// ------------

/// Add or remove progressive enhancement for links and forms
let boost (enabled : bool) = Attr.create "hx-boost" (if enabled then "true" else "false")

/// Pushes the URL into the browser location bar, creating a new history entry
let pushUrl (option : UrlOption) = Attr.create "hx-push-url" (UrlOption.AsString option)

/// Select content to swap in from a response
let select (option : TargetOption) = Attr.create "hx-select" (TargetOption.AsString option)

/// Select content to swap in from a response, out of band (somewhere other than the target)
let selectOob (option : TargetOption) = Attr.create "hx-select-oob" (TargetOption.AsString option)

/// Controls how content is swapped in (outerHTML, beforeEnd, afterend, ...)
let swap (option : SwapOption) = Attr.create "hx-swap" (SwapOption.AsString option)

/// Marks content in a response to be out of band (should swap in somewhere other than the target)
let swapOob (option : SwapOobOption) = Attr.create "hx-swap-oob" (SwapOobOption.AsString option)

/// Specifies the target element to be swapped
let target (option : TargetOption) = Attr.create "hx-target" (TargetOption.AsString option)

/// Specifies the event that triggers the request
let trigger (options : TriggerOption list) =
    options
    |> List.map TriggerOption.AsString
    |> fun x -> String.Join(", ", x)
    |> Attr.create "hx-trigger"

/// Adds values to the parameters to submit with the request (JSON-formatted)
let vals input =
    input
    |> fun x -> JsonSerializer.Serialize(x, Json.defaultSerializerOptions)
    |> Attr.create "hx-vals"

/// Adds values to the parameters to submit with the request (JSON-formatted)
let valsJs json = Attr.create "hx-vals" (String.Concat([ "js:"; json]))

// ------------
// Additional Attributes
// ------------

/// Shows a confim() dialog before issuing a request
let confirm = Attr.create "hx-confirm"

/// Disables htmx processing for the given node and any children nodes
let disable = Attr.createBool "hx-disable"

/// Control and disable automatic attribute inheritance for child nodes
let disinherit (option : DisinheritOption) = Attr.create "hx-disinherit" (DisinheritOption.AsString option)

/// Changes the request encoding type
let encoding = Attr.create "hx-encoding"

/// Extensions to use for this element
let ext = Attr.create "hx-ext"

/// Adds to the headers that will be submitted with the request
let headers (values : (string * string) list) =
    values
    |> Map.ofList
    |> fun x -> JsonSerializer.Serialize(x, Json.defaultSerializerOptions)
    |> Attr.create "hx-headers"

/// The element to snapshot and restore during history navigation
let historyElt = Attr.createBool "hx-history-elt"

/// Include additional data in requests
let include' (values : TargetOption list) =
    values
    |> List.map TargetOption.AsString
    |> fun x -> String.Join(", ", x)
    |> Attr.create "hx-include"

/// The element to put the htmx-request class on during the request
let indicator (option : TargetOption) = Attr.create "hx-indicator" (TargetOption.AsString option)

/// Filters the parameters that will be submitted with a request
let params' (option : ParamOption) = Attr.create "hx-params" (ParamOption.AsString option)

/// Specifies elements to keep unchanged between requests
let preserve = Attr.createBool "hx-preserve"

/// Shows a prompt() before submitting a request
let prompt = Attr.create "hx-prompt"

/// Replace the URL in the browser location bar
let replaceUrl (option : UrlOption) = Attr.create "hx-replace-url" (UrlOption.AsString option)

/// Configures various aspects of the request
let request = Attr.create "hx-request"

/// Control how requests made be different elements are synchronized
let sync (targetOption : TargetOption, syncOption : SyncOption option) =
    let attrValue =
        let target' = TargetOption.AsString targetOption

        match syncOption with
        | Some sync' -> String.Concat([ target'; ":"; SyncOption.AsString sync' ])
        | None -> target'

    Attr.create "hx-sync" attrValue

/// Whether to force elements to validate themselves before a request
let validate (value: bool) = Attr.create "hx-validate" (string value)

/// Whether to prevent sensitive data being saved to the history cache
let history (value: bool) = Attr.create "hx-history" (string value)

/// Handle any event with a script inline
let on eventName script = Attr.create $"hx-on:{eventName}"

/// adds the `disabled` attribute to the specified elements while a request is in flight
let disabledElement (targetOption: TargetOption) =
    Attr.create "hx-disabled-elt" (TargetOption.AsString targetOption)
