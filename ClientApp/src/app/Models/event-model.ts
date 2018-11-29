export class Date {
  month: number;
  day: number;
  year: number;
}

export class Time {
  hour: number;
  minute: number;
}

export class Appointment {
  id: number;
  display: string;
}

export class Event {
  startDate: Date | null;
  endDate: Date | null;
  name: string | null;
  address: string | null;
  description: string | null;
  startTime: Time | null;
  endTime: Time | null;
  image: string | null;
  appointments: Appointment[] | null;

}
