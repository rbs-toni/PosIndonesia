const fs = require("fs").promises;
const _ = require("lodash");

const dataPath = "./wwwroot/kodepos.json";
const outputPath = "./wwwroot/modified-kodepos.json";

const transformKodepos = async () => {
  try {
    console.group("Kodepos");
    const data = await fs.readFile(dataPath, "utf8");
    const jsonData = JSON.parse(data);

    const formatValue = (value) =>
      typeof value === "string"
        ? value.replace(/([a-z])([A-Z])/g, "$1 $2")
        : value;
    const renameKeys = {
      province: "Province",
      regency: "Regency",
      district: "District",
      village: "Village",
      latitude: "Latitude",
      longitude: "Longitude",
      elevation: "Elevation",
      timezone: "Timezone",
      code: "Code",
    };

    const transformObject = (obj) =>
      Object.fromEntries(
        Object.entries(obj).map(([key, value]) => [
          renameKeys[key] || key,
          formatValue(value),
        ])
      );

    kodepos = [...new Set(jsonData.map(JSON.stringify))]
      .map(JSON.parse)
      .map(transformObject);
    console.log(`Found: ${kodepos.length}`);
    await fs.writeFile(outputPath, JSON.stringify(kodepos, null, 2), "utf8");
    console.log(`Write to: ${outputPath}`);
    console.groupEnd();
  } catch (err) {
    console.error("Error processing kodepos:", err);
  }
};

const main = async () => {
  await transformKodepos();
};

main();
