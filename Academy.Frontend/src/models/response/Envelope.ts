export interface Envelope<T> {
  result?: T;
  errors?: ErrorList;
  timeGenerated: string;
}

export interface ErrorList {
  errors: Error[];
}

export interface Error {
  code: string;
  message: string;
  type: ErrorType;
  invalidField?: string;
}

export enum ErrorType {
  NotFound = 0,
  Validation = 1,
  Conflict = 2,
  Failure = 3,
  AccessDenied = 4,
  None = 5,
}
