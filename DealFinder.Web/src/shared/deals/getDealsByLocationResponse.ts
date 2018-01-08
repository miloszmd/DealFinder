export class GetDealsByLocationResponse {
    title: string;
    summary: any;
    distanceInMeters: number;
    location: Location;
}

export class Location {
    latitude: number;
    longitude: number;
}