export class Date {
  month: number;
  day: number;
  year: number;
}

export class Time {
  hour: number;
  minute: number;
}

export class Event {
  startDate: Date | null;
  endDate: Date | null;
  name: string | null;
  address: string | null;
  description: string | null;
  startTime: Time | null;

}
