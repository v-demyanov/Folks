import { formatInNN } from './number-helpers';

export function getUserFrendlyDateString(date: Date): string {
  const now = new Date();
  const monthDay = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();

  if (now.getFullYear() !== year) {
    return `${formatInNN(monthDay)}/${formatInNN(month)}/${year}`;
  }

  if (isThisWeek(date)) {
    return getWeekDayName(date);
  }

  return `${date.toLocaleDateString('default', { month: 'long' })} ${monthDay}`;
}

export function getWeekDayName(date: Date): string {
  const day = date.getDay();
  const weekDays = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ];

  return weekDays[day];
}

export function isThisWeek(date: Date): boolean {
  const todayObj = new Date();
  const todayDate = todayObj.getDate();
  const todayDay = todayObj.getDay();

  const firstDayOfWeek = new Date(todayObj.setDate(todayDate - todayDay));

  const lastDayOfWeek = new Date(firstDayOfWeek);
  lastDayOfWeek.setDate(lastDayOfWeek.getDate() + 6);

  return date >= firstDayOfWeek && date <= lastDayOfWeek;
}

export function formatInHHMM(date: Date): string {
  return `${formatInNN(date.getHours())}:${formatInNN(date.getMinutes())}`;
}
