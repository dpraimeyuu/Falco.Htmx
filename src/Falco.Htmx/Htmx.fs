namespace Falco.Htmx

open System
open Falco.Markup
open FSharp.Core

// module Triggers =
//     type [<Measure>] second

//     type QueueOption =
//         | First
//         | Last
//         | All
//         | None with

//         static member internal AsString (x : QueueOption) =
//             match x with
//             | First -> "first"
//             | Last -> "last"
//             | All -> "all"
//             | None -> "none"

//     type IntersectionThreshold = private IntersectionThreshold of float

//     let IntersectionThreshold threshold =
//         match threshold with
//         | threshold' when threshold' > 1.0 -> 1.0
//         | threshold' when threshold' < 0.0 -> 0.0
//         | _ -> threshold

//     type IntersectionOption =
//         | Root of selector : string
//         | Threshold of threshold : IntersectionThreshold with

//         static member internal AsString (x : IntersectionOption) =
//             match x with
//             | Root selector -> String.Concat([ "root:"; selector ])
//             | Threshold (IntersectionThreshold threshold) -> sprintf "threshold:%f" threshold

//     type HtmxTriggerModifier = private
//         | Once
//         | Every of double<second>
//         | Changed
//         | Delay of double<second>
//         | Throttle of double<second>
//         | Target of CSS_Selector
//         | Consume
//         | Queue of queueOption : QueueOption
//         | Load
//         | Revealed
//         | Intersect
//         | IntersectWith of intesectionOption : IntersectionOption
//         | FromBody with

//         static member internal AsString (x : HtmxTriggerModifier)
//             match this with
//             | Once -> "once"
//             | Changed -> "changed"
//             | Delay period -> $"delay:{period}s"
//             | Throttle period -> $"throttle:{period}s"
//             | Every period -> $"every {period}s"
//             | Target (CSS_Selector selector) -> $"target:{selector}"
//             | Consume -> "consume"
//             | Queue option -> String.Concat([ "queue:"; QueueOption.AsString option ])
//             | Intersect -> "intersect"
//             | IntersectWith option -> $"intersect:{option.ToString()}"
//             | Revealed -> "revealed"
//             | Load -> "load"
//             | FromBody -> "from:body"

//     let private joinWith (separator : string) (strs : string seq) =
//         String.Join(separator, strs)

//     let private mergeModifiers = joinWith " "

//     let private triggerModifiers (modifiers : HtmxTriggerModifier list) =
//         modifiers |> List.map (fun m -> m.ToString()) |> mergeModifiers

//     type ResponseEvent = ResponseEvent of eventName : string

//     type Trigger =
//         private
//         | Click
//         | MouseEnter
//         | Change
//         | Submit
//         | FromResponse of event : ResponseEvent

//         override this.ToString() =
//             match this with
//             | Click -> "click"
//             | MouseEnter -> "mouseenter"
//             | Change -> "change"
//             | Submit -> "submit"
//             | FromResponse (ResponseEvent eventName) -> $"{eventName}"

//         member this.WithModifiers(modifiers : HtmxTriggerModifier list) =
//             $"{this.ToString()} {triggerModifiers modifiers}"

//     module Modifiers =
//         let every (seconds : double<second>) = Every seconds
//         let once = Once
//         let changed = Changed
//         let delay (seconds : double<second>) = Delay seconds
//         let throttle (seconds : double<second>) = Throttle seconds
//         let target (selector : CSS_Selector) = Target selector
//         let consume = Consume
//         let queue (option : QueueOption) = Queue option
//         let intersect = Intersect
//         let intersectWith (option : IntersectionOption) = IntersectWith option
//         let revealed = Revealed
//         let load = Load


//     let click (modifier : HtmxTriggerModifier) = Click, modifier
//     let mouseenter (modifier : HtmxTriggerModifier) = MouseEnter, modifier
//     let change (modifier : HtmxTriggerModifier) = Change, modifier
//     let submit (modifier : HtmxTriggerModifier) = Submit, modifier

//     let fromResponse (responseEvent : ResponseEvent) =
//         FromResponse responseEvent, FromBody

// open Triggers

// type Triggers = (Trigger * HtmxTriggerModifier) list

// let trigger (triggers : Triggers) =
//     triggers
//     |> List.map (fun (trigger, modifier) ->
//         trigger.WithModifiers([ modifier ]))
//     |> List.fold
//         (fun triggers trigger ->
//             match triggers with
//             | "" -> trigger
//             | _ -> triggers + ", " + trigger)
//         ""
//     |> Attr.create "hx-trigger"

/// The hx-target attribute allows you to target a different element for swapping than the one issuing the AJAX request.
type TargetOption =
    private
    | This
    | CssSelector of string
    | Closest of string
    | Find of string with

    static member internal AsString (x : TargetOption) =
        match x with
        | This -> "this"
        | CssSelector selector -> selector
        | Closest selector -> String.Concat([ "closest "; selector ])
        | Find selector -> String.Concat([ "find "; selector ])

module Target =
    let this = This
    let cssSelect selector = CssSelector selector
    let closest selector = Closest selector
    let find selector = Find selector

/// The hx-swap attribute allows you to specify how the response will be swapped in relative to the target of an AJAX request.
type SwapOption =
    private
    | InnerHTML
    | OuterHTML
    | BeforeBegin
    | AfterBegin
    | BeforEend
    | AfterEnd
    | Delete
    | NoSwap with

    static member internal AsString (x : SwapOption) =
        match x with
        | InnerHTML -> "innerHTML"
        | OuterHTML -> "outerHTML"
        | BeforeBegin -> "beforebegin"
        | AfterBegin -> "afterbegin"
        | BeforEend -> "beforeend"
        | AfterEnd -> "afterend"
        | Delete -> "delete"
        | NoSwap -> "none"

module Swap =
    let innerHTML = InnerHTML
    let outerHTML = OuterHTML
    let beforebegin = BeforeBegin
    let afterbegin = AfterBegin
    let beforeend = BeforEend
    let afterend = AfterEnd
    let delete = Delete
    let none = NoSwap

type SwapOobOption =
    private
    | True
    | SwapOption of SwapOption
    | SwapOptionSelect of SwapOption * TargetOption with

    static member internal AsString (x : SwapOobOption) =
        match x with
        | True -> "true"
        | SwapOption swap -> SwapOption.AsString swap
        | SwapOptionSelect (swap, selector) ->
            String.Concat([ SwapOption.AsString swap; ":"; TargetOption.AsString selector ])

module SwapOob =
    let true' = True
    let swap (option : SwapOption) = SwapOption option
    let swapSelect (option : SwapOption * TargetOption) = SwapOptionSelect option

/// The hx-sync attribute allows you to synchronize AJAX requests between multiple elements.
///
/// The hx-sync attribute consists of a CSS selector to indicate the element to synchronize on, followed optionally by a colon and then by an optional syncing strategy.
type SyncQueueOption =
    private
    | First
    | Last
    | All with

    static member internal AsString (x : SyncQueueOption) =
        match x with
        | First -> "first"
        | Last -> "last"
        | All -> "all"

type SyncOption =
    private
    | Drop
    | Abort
    | Replace
    | Queue of SyncQueueOption with

    static member internal AsString (x : SyncOption) =
        match x with
        | Drop -> "drop"
        | Abort -> "abort"
        | Replace -> "replace"
        | Queue queue -> String.Concat([ "queue "; SyncQueueOption.AsString queue ])

module Sync =
    let drop = Drop
    let abort = Abort
    let replace = Replace
    let queueFirst = Queue First
    let queueLast = Queue Last
    let queueAll = Queue All

/// The HX-Push-Url header allows you to push a URL into the browser location history. This creates a new history entry, allowing navigation with the browser’s back and forward buttons. This is similar to the hx-push-url attribute.
///
/// If present, this header overrides any behavior defined with attributes.
type PushUrlOption =
    private
    | True
    | False
    | Url of string with

    static member internal AsString (x : PushUrlOption) =
        match x with
        | True -> "true"
        | False -> "false"
        | Url url -> url

module PushUrl =
    let true' = True
    let false' = False
    let url url' = Url url'

[<RequireQualifiedAccess>]
module Hx =
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
    let pushUrl (option : PushUrlOption) = Attr.create "hx-push-url" (PushUrlOption.AsString option)

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
    let trigger (option ) = Attr.create "hx-trigger"

    /// Adds values to the parameters to submit with the request (JSON-formatted)
    let vals = Attr.create "hx-vals"

    // ------------
    // Additional Attributes
    // ------------

    /// Shows a confim() dialog before issuing a request
    let confirm = Attr.create "hx-confirm"

    /// Disables htmx processing for the given node and any children nodes
    let disable = Attr.create "hx-disable"

    /// Control and disable automatic attribute inheritance for child nodes
    let disinherit = Attr.create "hx-disinherit"

    /// Changes the request encoding type
    let encoding = Attr.create "hx-encoding"

    /// Extensions to use for this element
    let ext = Attr.create "hx-ext"

    /// Adds to the headers that will be submitted with the request
    let headers = Attr.create "hx-headers"

    /// The element to snapshot and restore during history navigation
    let historyElt = Attr.create "hx-history-elt"

    /// Include additional data in requests
    let include' = Attr.create "hx-include"

    /// The element to put the htmx-request class on during the request
    let indicator = Attr.create "hx-indicator"

    /// Filters the parameters that will be submitted with a request
    let params' = Attr.create "hx-params"

    /// Specifies elements to keep unchanged between requests
    let preserve = Attr.create "hx-preserve"

    /// Shows a prompt() before submitting a request
    let prompt = Attr.create "hx-prompt"

    /// Replace the URL in the browser location bar
    let replaceUrl = Attr.create "hx-replace-url"

    /// Configures various aspects of the request
    let request = Attr.create "hx-request"

    /// Has been moved to an extension. Documentation for older versions
    let sse = Attr.create "hx-sse"

    /// Control how requests made be different elements are synchronized
    let sync (targetOption : TargetOption, syncOption : SyncOption option) =
        let attrValue =
            let target' = TargetOption.AsString targetOption

            match syncOption with
            | Some sync' -> ""
            | None -> target'

        Attr.create "hx-sync" attrValue

    /// Has been moved to an extension. Documentation for older versions
    let ws = Attr.create "hx-ws"
