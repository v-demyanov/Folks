import { formatInNN } from './number-helpers';
import IUserFrendlyDateOptions from '../models/user-frendly-date-options';

export function getUserFrendlyDateString(
  date: Date,
  options?: IUserFrendlyDateOptions,
): string {
  const now = new Date();
  const monthDay = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();

  if (now.getFullYear() !== year) {
    return `${formatInNN(monthDay)}/${formatInNN(month)}/${year}`;
  }

  if (isToday(date) && options?.formatInHHMM) {
    return formatInHHMM(date);
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

export function isToday(date: Date): boolean {
  const today = new Date();
  return today.toDateString() === date.toDateString();
}

export function formatInHHMM(date: Date): string {
  return `${formatInNN(date.getHours())}:${formatInNN(date.getMinutes())}`;
}
