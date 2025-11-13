export const toPascalCase = (str: string): string => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

export const toCamelCase = (str: string): string => {
  return str.charAt(0).toLowerCase() + str.slice(1);
};

export const convertKeysToPascalCase = <T extends Record<string, any>>(
  obj: T
): Record<string, any> => {
  if (obj === null || typeof obj !== "object") {
    return obj;
  }

  if (Array.isArray(obj)) {
    return obj.map((item) => convertKeysToPascalCase(item));
  }

  return Object.keys(obj).reduce((acc, key) => {
    const pascalKey = toPascalCase(key);
    const value = obj[key];

    acc[pascalKey] =
      typeof value === "object" && value !== null
        ? convertKeysToPascalCase(value)
        : value;

    return acc;
  }, {} as Record<string, any>);
};

export const convertKeysToCamelCase = <T extends Record<string, any>>(
  obj: T
): Record<string, any> => {
  if (obj === null || typeof obj !== "object") {
    return obj;
  }

  if (Array.isArray(obj)) {
    return obj.map((item) => convertKeysToCamelCase(item));
  }

  return Object.keys(obj).reduce((acc, key) => {
    const camelKey = toCamelCase(key);
    const value = obj[key];

    acc[camelKey] =
      typeof value === "object" && value !== null
        ? convertKeysToCamelCase(value)
        : value;

    return acc;
  }, {} as Record<string, any>);
};
