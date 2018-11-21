export class Image {
  id: string;
  imageBase64: string;

  constructor(image: Image) {
    this.id = image.id;
    this.imageBase64 = image.imageBase64;
  }

}
