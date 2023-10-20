export function getUserFrendlyDateString(date: Date): string {
  const now = new Date();
  const weekDay = date.getDay();
  const monthDay = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();

  if (now.getFullYear() !== year) {
    return `${monthDay}/${month}/${year}`;
  }

  if (isThisWeek(date)) {
    return getWeekDayName(weekDay);
  }

  return `${monthDay}/${month}`;
}

export function getWeekDayName(day: number): string {
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
