export type Misty = {
  RegisterEvent: (
    eventName: string,
    messageType: string,
    debounce: number,
    keepAlive?: boolean,
    callbackRule?:  "synchronous" | "override" | "abort",
    skillToCall?: string,
    prePauseMs?: number,
    postPauseMs?: number) => any;
  
  DisplayText: (text: string, layer?: string, prePauseMs?: number, postPauseMs?: number) => void;
  PlayAudio: (filename: string, volume: number, prePauseMs?: number, postPauseMs?: number) => void;
}

export type Question = {
  question: string;
  audioUrl: string;
  answers: string[];
  language: 'it' | 'en' | 'fr' | 'nl'
}