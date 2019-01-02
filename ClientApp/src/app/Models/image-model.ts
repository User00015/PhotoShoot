export class Image {
  id: string;
  imgUrl: string;

  constructor(image: Image) {
    this.id = image.id;
    this.imgUrl = image.imgUrl;
  }

}
