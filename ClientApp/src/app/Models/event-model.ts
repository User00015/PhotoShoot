export class Date  {
  month: number | null;
  day: number | null;
  year: number | null;
}

export class Time {
  hour: number | null;
  minute: number | null;
}

export class Appointment {
  id: number;
  display: string;
  open: boolean;
}

export class Event {
  id: string | null;
  name: string | null;
  address: string | null;
  description: string | null;
  image: string | null;
  appointments: Appointment[] | null;
  startDate: string | null;
  endDate: string | null;
}
