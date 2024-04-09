import ICardView from "../card/view/cardView";

export function toDictionary(array: ICardView[]){
    let cards = new Map<string, ICardView[]>;

    array.forEach(item => {
        const status = JSON.stringify(item.status);

        let val = cards.get(status);

        if(!val){
            cards.set(status, [item]);
        }else{
            cards.set(status, [...val, item])
        }
    })

    return Array.from(cards, ([status, card]) => ({status, card}));
}