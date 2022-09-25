namespace Falco.Htmx

open Falco.Markup
open System
open FSharp.Core
open Helpers

[<RequireQualifiedAccess>]
module Hx =
    let get ((Uri uri): Uri) =
        Attr.create "hx-get" uri
    let post ((Uri uri): Uri) =
        Attr.create "hx-post" uri
    let put ((Uri uri): Uri) =
        Attr.create "hx-put" uri
    let delete ((Uri uri): Uri) =
        Attr.create "hx-delete" uri
    let patch ((Uri uri): Uri) =
        Attr.create "hx-patch" uri    

    module Triggers =
            
            module Queue =
                type Option =
                | First
                | Last
                | All
                | None
                with
                    override this.ToString() =
                        match this with
                        | First -> "first"
                        | Last -> "last"
                        | All -> "all"
                        | None -> "none"
            module Intersection =
                open Microsoft.FSharp.Core
                type IntersectionThreshold = private IntersectionThreshold of float
                let IntersectionThreshold threshold =
                    match threshold with
                    | threshold' when threshold' > 1.0 -> 1.0
                    | threshold' when threshold' < 0.0 -> 0.0
                    | _ -> threshold
                type Option =
                | Root of selector: CSS_Selector
                | Threshold of threshold: IntersectionThreshold
                with
                    override this.ToString() =
                        match this with
                        | Root selector -> $"root:{selector}"
                        | Threshold (IntersectionThreshold threshold) -> $"threshold:{threshold}"

            type HtmxTriggerModifier =
                private
                | Once
                | Every of double<second>
                | Changed
                | Delay of double<second>
                | Throttle of double<second>
                | Target of CSS_Selector
                | Consume
                | Queue of queueOption: Queue.Option
                | Load
                | Revealed
                | Intersect
                | IntersectWith of intesectionOption: Intersection.Option
                | FromBody
                    with
                        override this.ToString() =
                            match this with
                            | Once -> "once"
                            | Changed -> "changed"
                            | Delay period -> $"delay:{period}s"
                            | Throttle period -> $"throttle:{period}s"
                            | Every period -> $"every {period}s"
                            | Target (CSS_Selector selector) -> $"target:{selector}"
                            | Consume -> "consume"
                            | Queue option -> $"queue:{option.ToString()}"
                            | Intersect -> "intersect"
                            | IntersectWith option -> $"intersect:{option.ToString()}"
                            | Revealed -> "revealed"
                            | Load -> "load"
                            | FromBody -> "from:body"       

            let private joinWith (separator: string) (strs: string seq) = String.Join(separator, strs)
            let private mergeModifiers = joinWith " "
            let private triggerModifiers (modifiers: HtmxTriggerModifier list) =
                modifiers
                |> List.map(fun m -> m.ToString())
                |> mergeModifiers

            type ResponseEvent = ResponseEvent of eventName: string
            type Trigger = 
                private
                | Click
                | MouseEnter
                | Change
                | Submit
                | FromResponse of event: ResponseEvent
                with
                    override this.ToString() =
                        match this with
                        | Click -> "click"
                        | MouseEnter -> "mouseenter"
                        | Change -> "change"
                        | Submit -> "submit"
                        | FromResponse (ResponseEvent eventName) -> $"{eventName}"
                    member this.WithModifiers(modifiers: HtmxTriggerModifier list) =
                        $"{this.ToString()} {triggerModifiers modifiers}"
            module Modifiers =
                let every (seconds: double<second>) = Every seconds
                let once = Once
                let changed = Changed
                let delay (seconds: double<second>) = Delay seconds
                let throttle (seconds: double<second>) = Throttle seconds
                let target (selector: CSS_Selector) = Target selector
                let consume = Consume
                let queue (option: Queue.Option) = Queue option
                let intersect = Intersect
                let intersectWith (option: Intersection.Option) = IntersectWith option
                let revealed = Revealed
                let load = Load


            let click (modifier: HtmxTriggerModifier) =
                Click, modifier
            let mouseenter (modifier: HtmxTriggerModifier) =  
                MouseEnter, modifier
            let change (modifier: HtmxTriggerModifier) =
                Change, modifier
            let submit (modifier: HtmxTriggerModifier) =
                Submit, modifier
            let fromResponse (responseEvent: ResponseEvent) =
                FromResponse responseEvent, FromBody

    open Triggers

    type Triggers = (Trigger * HtmxTriggerModifier) list
    let trigger (triggers: Triggers) =
        triggers
        |> List.map (fun (trigger, modifier) ->
            trigger.WithModifiers ([modifier])
        )
        |> List.fold (fun triggers trigger ->
            match triggers with
            | "" -> trigger
            | _ -> triggers + ", " + trigger
        ) ""
        |> Attr.create "hx-trigger"

    [<AutoOpen>]
    module Swap =
        type SwapOption =
            private
            | InnerHTML
            | OuterHTML
            | BeforeBegin
            | AfterBegin
            | BeforEend
            | AfterEnd
            | Delete
            | None
            with
                override this.ToString() =
                    match this with
                    | InnerHTML -> "innerHTML"
                    | OuterHTML -> "outerHTML"
                    | BeforeBegin -> "beforebegin"
                    | AfterBegin -> "afterbegin"
                    | BeforEend -> "beforeend"
                    | AfterEnd -> "afterend"
                    | Delete -> "delete"
                    | None -> "none"
                    
        let innerHTML = InnerHTML
        let outerHTML = OuterHTML
        let beforebegin = BeforeBegin
        let afterbegin = AfterBegin
        let beforeend = BeforEend
        let afterend = AfterEnd
        let delete = Delete
        let none = None
    open Swap
    let swap (option: SwapOption) =
        Attr.create "hx-swap" (option.ToString())

    let target ((CSS_Selector selector): CSS_Selector) =
        Attr.create "hx-target" selector
    