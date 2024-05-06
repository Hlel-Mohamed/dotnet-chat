import { EventEmitter } from "@angular/core";

/**
 * Emitters is a class that provides a static EventEmitter for authentication events.
 * This allows different parts of the application to subscribe to and emit authentication events.
 */
export class Emitters {
    /**
     * authEmitter is a static EventEmitter that emits boolean values.
     * A value of true represents an authenticated state, and a value of false represents an unauthenticated state.
     */
    static authEmitter = new EventEmitter<boolean>();
}
